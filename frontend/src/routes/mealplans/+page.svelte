<script lang="ts">
	import { AppBar, getModalStore, type ModalSettings } from '@skeletonlabs/skeleton';
	import { getAllMeals, type Meal } from 'api/meals';
	import PrimaryButton from 'components/buttons/PrimaryButton.svelte';
	import { format, getDaysInMonth, isSameDay } from 'date-fns';
	import { sortAsc, sortByDate } from 'utils/sortUtils';
	import MonthSelector from 'components/date/MonthSelector.svelte';

	const modalStore = getModalStore();

	const modal: ModalSettings = {
		type: 'component',
		component: 'createMealDialog',
	};

	$: meals = getAllMeals();

	const currentDate = new Date();

	let selectedMonth = currentDate.getMonth();
	let selectedYear = currentDate.getFullYear();

	$: selectedDate = new Date(selectedYear, selectedMonth);
	$: dayCount = getDaysInMonth(selectedDate);

	const handleDialogOpen = (): void => {
		modalStore.trigger(modal);
	};

	const handleDialogOpenForDay = (day: number): void => {
		const date = new Date();
		date.setDate(day);
		modalStore.trigger({ ...modal, meta: { at: date } });
	};

	const getMealsForDay = (day: number): Meal[] => {
		if (!$meals.data || $meals.data.length === 0) {
			return [];
		}
		const copiedDate = new Date(selectedDate);
		copiedDate.setDate(day);

		let foundMeals = $meals.data?.filter((m) => isSameDay(m.at, copiedDate));
		foundMeals = foundMeals.sort((a, b) => sortByDate(a.at, b.at, sortAsc));
		return foundMeals ?? [];
	};

	const isToday = (day: number): boolean => {
		const now = new Date();
		if (selectedYear != now.getFullYear()) {
			return false;
		}

		if (selectedMonth != now.getMonth()) {
			return false;
		}

		return now.getDate() === day;
	};
</script>

<AppBar class="mb-4">
	<svelte:fragment slot="lead"><h4 class="h4">Meal Plan</h4></svelte:fragment>
	<svelte:fragment>
		<div class="grid grid-cols-2 grid-rows-1 gap-2">
			<MonthSelector bind:value={selectedMonth} />
			<div class="">
				<label class="label">
					<span>Year</span>
					<input type="number" class="input rounded-lg p-1.5" bind:value={selectedYear} />
				</label>
			</div>
		</div>
	</svelte:fragment>
	<svelte:fragment slot="trail">
		<PrimaryButton text="Add Meal" onClick={handleDialogOpen} />
	</svelte:fragment>
</AppBar>

<div>
	<div class="grid grid-cols-7 gap-1">
		{#each Array(dayCount) as _, dayIndex}
			<div
				class="z-0 h-40 rounded-lg bg-emerald-100 transition-colors hover:bg-emerald-500 dark:bg-emerald-900 dark:hover:bg-emerald-700"
				role="cell"
				tabindex={dayIndex + 1}
				on:click={() => handleDialogOpenForDay(dayIndex + 1)}
				on:keydown={(k) => {
					if (k.code === 'Enter') {
						handleDialogOpenForDay(dayIndex + 1);
						return;
					}
				}}
			>
				<div class="grid grid-cols-1 gap-1 p-2">
					<div>
						{dayIndex + 1}
						{#if isToday(dayIndex + 1)}TODAY{/if}
					</div>
					{#if $meals.isSuccess && !$meals.isRefetching}
						{#key selectedDate}
							{#each getMealsForDay(dayIndex + 1) as meal}
								<div
									class="z-10 rounded-lg bg-sky-300 p-1 transition-colors hover:bg-sky-500 dark:bg-sky-600"
									role="cell"
									tabindex={dayIndex + 1}
									on:click={(mouseEvent) => mouseEvent.stopPropagation()}
									on:keydown={(keyEvent) => keyEvent.stopPropagation()}
								>
									{meal.recipe.name}
									{format(meal.at, 'HH:mm')}
								</div>
							{/each}
						{/key}
					{/if}
				</div>
			</div>
		{/each}
	</div>
</div>
