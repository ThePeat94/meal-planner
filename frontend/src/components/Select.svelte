<script lang="ts">
	export let value: unknown | undefined;
	export let options: { label: string; value: unknown }[];
	export let label: string;
</script>

<label class="label">
	<span>{label}</span>
	<select class="select p-2" bind:value>
		{#each options as option}
			<option value={option.value}>{option.label}</option>
		{/each}
	</select>
</label>
