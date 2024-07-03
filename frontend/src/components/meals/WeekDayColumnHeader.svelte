<script lang="ts">
	export let dayIndex: number;

	const weekDayIdx: Record<number, string> = {
		0: 'Monday',
		1: 'Tuesday',
		2: 'Wednesday',
		3: 'Thursday',
		4: 'Friday',
		5: 'Saturday',
		6: 'Sunday',
	};
</script>

<div class="rounded-lg bg-amber-100 p-2 dark:bg-amber-900">{weekDayIdx[dayIndex]}</div>
