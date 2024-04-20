<script lang="ts">
	import { AppBar } from '@skeletonlabs/skeleton';
	import { getRecipe, updateRecipe } from 'api/recipes';
	import PrimaryButton from 'components/buttons/PrimaryButton.svelte';
	import type { RecipePageData } from 'routes/recipes/[id]/+page';

	export let data: RecipePageData;

	const mutateRecipe = updateRecipe();

	$: recipe = getRecipe(data.id);

	$: tmpRecipe = $recipe.data;

	const handleSave = (): void => {
		$mutateRecipe.mutate(tmpRecipe!);
	};

	const handleDescriptionUpdate = (newDescription: string): void => {
		if (!tmpRecipe) {
			return;
		}

		tmpRecipe.description = newDescription;
	};
</script>

{#if $recipe.isSuccess && tmpRecipe}
	<AppBar class="mb-4">
		<svelte:fragment slot="lead"><h4 class="h4">Recipe > {tmpRecipe?.name}</h4></svelte:fragment>
		<svelte:fragment slot="trail">
			<PrimaryButton text={'Save'} onClick={handleSave} />
		</svelte:fragment>
	</AppBar>
	<div class="grid grid-cols-4 gap-4">
		<div class="relative col-span-2 col-start-2 mt-2 rounded-md shadow-sm">
			<label for="name" class="block text-sm font-medium leading-6 text-black dark:text-white">
				Name
			</label>
			<input
				type="text"
				name={'name'}
				id={'name'}
				class="block w-full rounded-md border-0 py-1.5 pl-2 pr-2 text-gray-900 ring-1 ring-inset ring-gray-300
            placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
				bind:value={tmpRecipe.name}
				autocomplete={'off'}
			/>
		</div>
		<div class="relative col-span-2 col-start-2 mt-2 rounded-md shadow-sm">
			<label
				for="description"
				class="block text-sm font-medium leading-6 text-black dark:text-white"
			>
				Description
			</label>
			<textarea
				name={'description'}
				id={'description'}
				class="block h-48 w-full border-0 py-1.5 pl-2 pr-2 text-gray-900 ring-1 ring-inset ring-gray-300
            placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
				value={tmpRecipe?.description}
				on:change={(e) => handleDescriptionUpdate(e.currentTarget.value)}
			/>
		</div>
	</div>
{/if}
