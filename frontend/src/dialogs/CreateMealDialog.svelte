<script lang="ts">
	import type { SvelteComponent } from 'svelte';

	// Stores
	import { getModalStore } from '@skeletonlabs/skeleton';
	import PrimaryButton from 'components/buttons/PrimaryButton.svelte';
	import ErrorButton from 'components/buttons/ErrorButton.svelte';
	import { createMeal } from 'api/meals';
	import { getAllRecipes, type Recipe } from 'api/recipes';
	import { format } from 'date-fns';
	import Select from 'components/Select.svelte';

	// Props
	/** Exposes parent props to this component. */
	export let parent: SvelteComponent;

	const modalStore = getModalStore();
	const createMealMutation = createMeal();
	const allRecipes = getAllRecipes();
	$: recipeOptions =
		$allRecipes.data?.map((r) => ({
			label: r.name,
			value: r,
		})) ?? [];

	type FormCreation = {
		at?: string;
		recipe?: Recipe;
	};

	const formData: FormCreation = {
		at: $modalStore[0].meta?.at ? format($modalStore[0].meta.at, "yyyy-MM-dd'T'HH:mm") : undefined,
		recipe: $modalStore[0].meta?.recipe,
	};

	// We've created a custom submit function to pass the response and close the modal.
	const handleCreation = (): void => {
		console.log(formData);
		if (!formData.at || !formData.recipe) {
			return;
		}

		$createMealMutation.mutate({ at: formData.at, recipe: formData.recipe });
		modalStore.close();
	};

	const handleClose = (): void => {
		modalStore.close();
	};

	// Base Classes
	const cBase = 'card p-4 w-modal shadow-xl space-y-4';
	const cHeader = 'text-2xl font-bold';
	const cForm = 'border border-surface-500 p-4 space-y-4 rounded-container-token';
</script>

<!-- @component This example creates a simple form modal. -->
{#if $modalStore[0]}
	<div class="modal-example-form {cBase}">
		<header class={cHeader}>Add a Meal</header>
		<!-- Enable for debugging: -->
		<form class="modal-form {cForm}">
			<Select options={recipeOptions} bind:value={formData.recipe} label={'Recipes'} />
			<label class="label">
				<span>Date</span>
				<input type="datetime-local" class="input p-2" bind:value={formData.at} />
			</label>
		</form>
		<footer class="modal-footer {parent.regionFooter}">
			<ErrorButton text="Cancel" isGhost={true} onClick={handleClose} />
			<PrimaryButton text="Create" onClick={handleCreation} />
		</footer>
	</div>
{/if}
