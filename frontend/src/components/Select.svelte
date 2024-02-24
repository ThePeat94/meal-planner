<script lang="ts" generics="T">
	import type { SelectOption } from 'components/types/SelectOption';

	// eslint-disable-next-line no-undef
	export let value: T | undefined;
	// eslint-disable-next-line no-undef
	export let options: SelectOption<T>[];
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
