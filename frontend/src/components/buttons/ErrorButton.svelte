<script lang="ts">
	export let text: string;
	export let onClick: () => void = () => {};
</script>

<button type="button" class="variant-filled-error btn" name={text} on:click={() => onClick()}>
	{text}
</button>
