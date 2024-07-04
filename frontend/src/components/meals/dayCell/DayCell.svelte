<script lang="ts">
	import type { DayCellInfo } from 'components/meals/dayCell/type';
	import { createEventDispatcher } from 'svelte';
	import { format, isToday } from 'date-fns';

	const dispatch = createEventDispatcher();

	export let dayCellInfo: DayCellInfo;
	export let tabIndex: number = 0;
	export let isSameMonth: boolean = true;

	const dispatchCreateMealEvent = (): void => {
		dispatch('createMeal');
	};

	const handleClick = (): void => {
		dispatchCreateMealEvent();
	};

	const handleKeyDown = (k: KeyboardEvent): void => {
		if (k.code !== 'Enter') {
			return;
		}
		dispatchCreateMealEvent();
	};
</script>

<div
	class="z-0 h-40 rounded-lg transition-colors hover:bg-emerald-500 dark:hover:bg-emerald-700"
	role="cell"
	on:click|stopPropagation={handleClick}
	on:keydown|stopPropagation={handleKeyDown}
	tabindex={tabIndex}
	class:dark:bg-emerald-900={isSameMonth}
	class:dark:bg-gray-700={!isSameMonth}
	class:bg-emerald-100={isSameMonth}
	class:bg-gray-300={!isSameMonth}
>
	<div class="grid grid-cols-1 gap-1 p-2">
		<div>
			{dayCellInfo.date.getDate()}
			{#if isToday(dayCellInfo.date)}TODAY{/if}
		</div>
		{#each dayCellInfo.meals as meal}
			<div
				class="z-10 rounded-lg bg-sky-300 p-1 transition-colors hover:bg-sky-500 dark:bg-sky-600"
				role="cell"
				on:click|stopPropagation
				on:keydown|stopPropagation
				tabindex={0}
			>
				{meal.recipe.name}
				{format(meal.at, 'HH:mm')}
			</div>
		{/each}
	</div>
</div>
