<script lang="ts">
	export let text: string;
	export let isGhost: boolean = false;
	export let onClick: () => void = () => {};

	const appliedClass = isGhost ? 'variant-ghost-error' : 'variant-filled-error';
</script>

<button type="button" class="{appliedClass} btn" name={text} on:click={() => onClick()}>
	{text}
</button>
