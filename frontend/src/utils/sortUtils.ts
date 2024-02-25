export const sortByDate = <T extends Date | string>(
	a: T,
	b: T,
	sort: (a: number, b: number) => number,
): number => {
	return sort(ensureDate(a).valueOf(), ensureDate(b).valueOf());
};

export const sortAsc = (a: number, b: number): number => {
	return a - b;
};

export const sortDesc = (a: number, b: number): number => {
	return b - a;
};

const ensureDate = (date: Date | string): Date => {
	if (typeof date === 'string') {
		return new Date(date);
	}
	return date;
};
