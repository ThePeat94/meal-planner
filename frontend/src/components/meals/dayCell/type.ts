import type { Meal } from 'api/meals';

export type DayCellInfo = {
	date: Date;
	meals: Meal[];
};
