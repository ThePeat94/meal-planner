<script lang="ts">
	export let onClick: () => void = () => {};
</script>

<div
	class="card card-hover"
	role="cell"
	tabindex={1}
	on:click={onClick}
	on:keydown={(k) => {
		if (k.code === 'Enter') {
			onClick();
		}
	}}
>
	<header class="card-header">
		<slot name="header" />
	</header>
	<section class="p-4">
		<slot name="content" />
	</section>
	{#if $$slots.footer}
		<footer class="card-footer grid grid-cols-2">
			<slot name="footer" />
		</footer>
	{/if}
</div>
