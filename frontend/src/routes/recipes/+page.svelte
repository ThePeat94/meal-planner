<script lang="ts">
	import { getAllRecipes } from "../../api/recipes";

    const query = getAllRecipes();
</script>

Hello this is the recipe list

{#if $query.isLoading}
    joa ich lade noch neee
{/if}
{#if $query.isError}
    joa, da warn error
{/if}
{#if $query.isSuccess}
    joa, da warn success, hier sind die daten: <br/>
    {#each $query.data as recipe}
        {recipe.id} {recipe.name} {recipe.description ?? 'nutting :('} <br/>
    {/each}
{/if}
