<script lang="ts">
	import { AppBar, getModalStore, type ModalSettings } from '@skeletonlabs/skeleton';
	import { getAllMeals, type Meal } from 'api/meals';
	import PrimaryButton from 'components/buttons/PrimaryButton.svelte';
	import { addDays, endOfMonth, getDaysInMonth, getISODay, isSameDay, isSameMonth } from 'date-fns';
	import { sortAsc, sortByDate } from 'utils/sortUtils';
	import MonthSelector from 'components/date/MonthSelector.svelte';
	import WeekDayColumnHeader from 'components/meals/WeekDayColumnHeader.svelte';
	import type { DayCellInfo } from 'components/meals/dayCell/type';
	import DayCell from 'components/meals/dayCell/DayCell.svelte';

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

	const handleDialogOpenForDay = (date: Date): void => {
		modalStore.trigger({ ...modal, meta: { at: date } });
	};

	const getMealsForDate = (date: Date): Meal[] => {
		if (!$meals.data || $meals.data.length === 0) {
			return [];
		}

		let foundMeals = $meals.data?.filter((m) => isSameDay(m.at, date));
		foundMeals = foundMeals.sort((a, b) => sortByDate(a.at, b.at, sortAsc));
		return foundMeals ?? [];
	};

	const createDayCards = (): DayCellInfo[] => {
		const dayCardInfo: DayCellInfo[] = [];
		const offsetPreviousMonths = getISODay(selectedDate) - 1;
		const offsetNextMonth = 7 - getISODay(endOfMonth(selectedDate));
		for (let i = -offsetPreviousMonths; i < dayCount + offsetNextMonth; i++) {
			const cardDate = addDays(selectedDate, i);

			dayCardInfo.push({
				date: cardDate,
				meals: getMealsForDate(cardDate),
			});
		}

		return dayCardInfo;
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
		{#each Array(7) as _, dayIdx}
			<WeekDayColumnHeader dayIndex={dayIdx} />
		{/each}
		{#key selectedDate}
			{#each createDayCards() as dayCard, dayIndex (dayCard.date)}
				<DayCell
					dayCellInfo={dayCard}
					isSameMonth={isSameMonth(dayCard.date, selectedDate)}
					tabIndex={dayIndex}
					on:createMeal={() => handleDialogOpenForDay(dayCard.date)}
				/>
			{/each}
		{/key}
	</div>
</div>
