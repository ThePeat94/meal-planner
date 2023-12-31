<script lang="ts">
	import { AppBar, getModalStore, type ModalSettings } from '@skeletonlabs/skeleton';
	import { getAllMeals, type Meal } from 'api/meals';
	import PrimaryButton from 'components/buttons/PrimaryButton.svelte';
	import { format, getDaysInMonth, isSameDay } from 'date-fns';

	const modalStore = getModalStore();

	const modal: ModalSettings = {
		type: 'component',
		component: 'createMealDialog',
	};

	const handleDialogOpen = (): void => {
		modalStore.trigger(modal);
	};

	const handleDialogOpenForDay = (day: number): void => {
		const date = new Date();
		date.setDate(day);
		modalStore.trigger({ ...modal, meta: { at: date } });
	};

	$: meals = getAllMeals();
	const getMealsForDay = (day: number): Meal[] => {
		if (!$meals.data || $meals.data.length === 0) {
			return [];
		}
		const date = new Date();
		date.setDate(day);

		const foundMeal = $meals.data?.filter((m) => isSameDay(m.at, date));

		return foundMeal ?? [];
	};

	const currentDate = new Date();
	const dayCount = getDaysInMonth(currentDate);
</script>

<AppBar class="mb-4">
	<svelte:fragment slot="lead"><h4 class="h4">Meal Plan</h4></svelte:fragment>
	<svelte:fragment slot="trail">
		<PrimaryButton text="Add Meal" onClick={handleDialogOpen} />
	</svelte:fragment>
</AppBar>

{#if $meals.isSuccess && !$meals.isRefetching}
	<div>
		<div class="grid grid-cols-7 gap-1">
			{#each Array(dayCount) as _, day}
				<div
					class="z-0 h-40 rounded-lg bg-emerald-100 transition-colors hover:bg-emerald-500"
					role="cell"
					tabindex={day + 1}
					on:click={() => handleDialogOpenForDay(day + 1)}
					on:keydown={(k) => {
						if (k.code === 'Enter') {
							handleDialogOpenForDay(day + 1);
						}
					}}
				>
					<div class="grid grid-cols-1 gap-1 p-2">
						<div>
							{day + 1}
						</div>
						{#each getMealsForDay(day + 1) as meal}
							<div
								class="z-10 rounded-lg bg-sky-300 p-1 transition-colors hover:bg-sky-500"
								role="cell"
								tabindex={day + 1}
								on:click={(mouseEvent) => mouseEvent.stopPropagation()}
								on:keydown={(keyEvent) => keyEvent.stopPropagation()}
							>
								{meal.recipe.name}
								{format(meal.at, 'HH:mm')}
							</div>
						{/each}
					</div>
				</div>
			{/each}
		</div>
	</div>
{/if}
