<script lang="ts">
	import { goto } from '$app/navigation';
	import { getModalStore } from '@skeletonlabs/skeleton';
	import { AppBar, type ModalSettings } from '@skeletonlabs/skeleton';
	import { getAllRecipes, type Recipe } from 'api/recipes';
	import Card from 'components/Card.svelte';
	import PrimaryButton from 'components/buttons/PrimaryButton.svelte';
	import RecipeCard from 'components/recipes/RecipeCard.svelte';

	const modalStore = getModalStore();
	const modal: ModalSettings = {
		type: 'component',
		component: 'createRecipeDialog',
	};

	const mealModal: ModalSettings = {
		type: 'component',
		component: 'createMealDialog',
	};

	const handleModalOpen = (): void => {
		modalStore.trigger(modal);
	};

	const handleCreateMealForRecipe = (recipe: Recipe): void => {
		modalStore.trigger({ ...mealModal, meta: { recipe } });
	};

	const handleCardClick = (recipe: Recipe): void => {
		goto('/recipes/' + recipe.id);
	};

	$: query = getAllRecipes();
</script>

<AppBar class="mb-4">
	<svelte:fragment slot="lead">Recipes</svelte:fragment>
	<svelte:fragment slot="trail">
		<PrimaryButton text="Create Recipe" onClick={handleModalOpen} />
	</svelte:fragment>
</AppBar>

{#if $query.isLoading}
	<div class="grid grid-cols-4 gap-4">
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
			<RecipeCard
				{recipe}
				onClick={() => handleCreateMealForRecipe(recipe)}
				onCardClick={() => handleCardClick(recipe)}
			/>
		{/each}
	</div>
{/if}
