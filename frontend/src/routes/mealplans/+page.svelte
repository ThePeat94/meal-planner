<script lang="ts">
	import { AppBar, getModalStore, type ModalSettings } from '@skeletonlabs/skeleton';
	import PrimaryButton from 'components/buttons/PrimaryButton.svelte';
	import { getDaysInMonth } from 'date-fns';

	const modalStore = getModalStore();

	const modal: ModalSettings = {
		type: 'component',
		component: 'createMealDialog',
	};

	const handleDialogOpen = (): void => {
		modalStore.trigger(modal);
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

<div>
	<div class="grid grid-cols-7 gap-1">
		{#each Array(dayCount) as _, day}
			<div class="h-40 rounded-lg bg-emerald-100 transition-colors hover:bg-emerald-500">
				{day + 1}
			</div>
		{/each}
	</div>
</div>
