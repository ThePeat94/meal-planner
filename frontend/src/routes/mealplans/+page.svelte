<script>
	import { AppBar } from '@skeletonlabs/skeleton';
	import { getDaysInMonth } from 'date-fns';

	const currentDate = new Date();

	const dayCount = getDaysInMonth(currentDate);
</script>

<AppBar class="mb-4">
	<svelte:fragment slot="lead"><h4 class="h4">Meal Plan</h4></svelte:fragment>
</AppBar>

<div>
	<div class="grid grid-cols-7 gap-1">
		{#each Array(dayCount) as _, day}
			<div class="h-40 rounded-lg bg-emerald-100 transition-colors hover:bg-emerald-500">
				{day + 1}
				<div class="grid-cols-1">
					<div>Foo 1</div>
					<div>Foo 2</div>
					<div>Foo 3</div>
				</div>
			</div>
		{/each}
	</div>
</div>
