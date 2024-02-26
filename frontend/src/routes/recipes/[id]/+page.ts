export const ssr = false;

export type RecipePageData = {
	id: number;
};

// @ts-expect-error dunno yet what type params is
export const load: RecipePageData = ({ params }) => {
	return {
		id: params.id,
	};
};
