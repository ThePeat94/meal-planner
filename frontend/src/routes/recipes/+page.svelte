<script lang="ts">
	import { getModalStore } from '@skeletonlabs/skeleton';
	import { AppBar, type ModalSettings } from '@skeletonlabs/skeleton';
	import { getAllRecipes } from 'api/recipes';
	import Card from 'components/Card.svelte';
	import PrimaryButton from 'components/buttons/PrimaryButton.svelte';

	const modalStore = getModalStore();
	const modal: ModalSettings = {
		type: 'component',
		component: 'createRecipeDialog',
	};

	const handleModalOpen = (): void => {
		modalStore.trigger(modal);
	};

	const query = getAllRecipes();
</script>

<AppBar class="mb-4">
	<svelte:fragment slot="lead">Recipe leads</svelte:fragment>
	<svelte:fragment slot="trail">
		<PrimaryButton text="Create Recipe" onClick={handleModalOpen} />
	</svelte:fragment>
</AppBar>

{#if $query.isLoading}
	<div class="grid grid-cols-2 gap-4">
		{#each Array(20) as _}
			<Card>
				<div slot="header" class="placeholder animate-pulse"></div>
				<div slot="content" class="placeholder animate-pulse"></div>
			</Card>
		{/each}
	</div>
{/if}
{#if $query.isError}
	<aside class="alert variant-filled-error">
		<div class="alert-message">
			<h3 class="h3">Error loading recipes</h3>
			<p>Satz mit x, war wohl nix</p>
		</div>
	</aside>
{/if}
{#if $query.isSuccess}
	<div class="grid grid-cols-2 gap-4">
		{#each $query.data as recipe}
			{#if recipe.description}
				<Card>
					<div slot="header">{recipe.id}</div>
					<div slot="content">{recipe.name}</div>
					<div slot="footer">{recipe.description}</div>
				</Card>
			{:else}
				<Card>
					<div slot="header">{recipe.id}</div>
					<div slot="content">{recipe.name}</div>
				</Card>
			{/if}
		{/each}
	</div>
{/if}
