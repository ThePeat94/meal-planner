<div class="card">
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
