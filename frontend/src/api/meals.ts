import {
	createQuery,
	type CreateMutationResult,
	type CreateQueryResult,
	useQueryClient,
	createMutation,
} from '@tanstack/svelte-query';
import type { Recipe } from 'api/recipes';
import type { AxiosRequestConfig } from 'axios';
import axios from 'axios';

export type Meal = {
	id?: number;
	at: string;
	recipe: Recipe;
};

const fetchMeals = async (config?: AxiosRequestConfig): Promise<Meal[]> => {
	const request = await axios.get<Meal[]>('http://localhost:5190/meals', { ...config });
	return request.data;
};

export const getAllMeals = (): CreateQueryResult<Meal[]> => {
	return createQuery({
		queryKey: ['meals'],
		queryFn: async ({ signal }) => await fetchMeals({ signal }),
	});
};

const postMeal = async (meal: Meal, config?: AxiosRequestConfig): Promise<Meal> => {
	const request = await axios.post<Meal>('http://localhost:5190/meals', meal, { ...config });
	return request.data;
};

export const createMeal = (): CreateMutationResult<Meal, Error, Meal> => {
	const client = useQueryClient();
	return createMutation<Meal, Error, Meal>({
		mutationFn: async (m) => await postMeal(m),
		onSettled: () => client.invalidateQueries({ queryKey: ['meals'] }),
	});
};
