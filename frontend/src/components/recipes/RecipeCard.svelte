<script lang="ts">
	import type { Recipe } from 'api/recipes';
	import Card from 'components/Card.svelte';
	import PrimaryButton from 'components/buttons/PrimaryButton.svelte';

	export let recipe: Recipe;
	export let onClick: () => void = () => {};
	export let onCardClick: () => void = () => {};
</script>

<Card onClick={onCardClick}>
	<div slot="header">{recipe.id}</div>
	<div slot="content">
		{recipe.name}
		{#if recipe.description}
			{recipe?.description}
		{/if}
	</div>
	<div slot="footer">
		<PrimaryButton {onClick} text={'Create Meal'} />
	</div>
</Card>
