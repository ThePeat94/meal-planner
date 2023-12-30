<script lang="ts">
	import type { SvelteComponent } from 'svelte';

	// Stores
	import { getModalStore } from '@skeletonlabs/skeleton';
	import PrimaryButton from 'components/buttons/PrimaryButton.svelte';
	import ErrorButton from 'components/buttons/ErrorButton.svelte';
	import { createRecipe } from 'api/recipes';

	// Props
	/** Exposes parent props to this component. */
	export let parent: SvelteComponent;

	const modalStore = getModalStore();

	const createRecipeMutation = createRecipe();

	// Form Data
	const formData = {
		name: '',
	};

	// We've created a custom submit function to pass the response and close the modal.
	const handleCreation = (): void => {
		$createRecipeMutation.mutate({ name: formData.name });
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
		<header class={cHeader}>Create a Recipe</header>
		<!-- Enable for debugging: -->
		<form class="modal-form {cForm}">
			<label class="label">
				<span>Name</span>
				<input
					class="input p-2"
					type="text"
					placeholder="Enter Recipe name..."
					bind:value={formData.name}
				/>
			</label>
		</form>
		<footer class="modal-footer {parent.regionFooter}">
			<ErrorButton text="Cancel" isGhost={true} onClick={handleClose} />
			<PrimaryButton text="Create" onClick={handleCreation} />
		</footer>
	</div>
{/if}
