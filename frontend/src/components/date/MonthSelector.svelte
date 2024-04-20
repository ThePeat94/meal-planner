<script lang="ts">
	import Select from 'components/Select.svelte';
	import type { SelectOption } from 'components/types/SelectOption';

	export let value: number;

	const monthOptions: SelectOption<number>[] = [
		{
			label: 'January',
			value: 0,
		},
		{
			label: 'February',
			value: 1,
		},
		{
			label: 'March',
			value: 2,
		},
		{
			label: 'April',
			value: 3,
		},
		{
			label: 'May',
			value: 4,
		},
		{
			label: 'June',
			value: 5,
		},
		{
			label: 'July',
			value: 6,
		},
		{
			label: 'August',
			value: 7,
		},
		{
			label: 'September',
			value: 8,
		},
		{
			label: 'October',
			value: 9,
		},
		{
			label: 'November',
			value: 10,
		},
		{
			label: 'December',
			value: 11,
		},
	];
</script>

<Select label="Month" options={monthOptions} bind:value />
