<template>
    <li>
        <span v-bind:class="{done: todo.completed}" v-if="!todo.edit">
            <input type="checkbox" v-model="todo.completed" v-on:change="$emit('edit-todo', todo)">
            <strong>{{index + 1}}</strong>
            {{todo.title}}
        </span>
        <div v-if="todo.edit">
            <textarea v-model="todo.title"></textarea>
        </div>
        <button v-on:click="$set(todo, 'edit', !todo.edit)" v-if="!todo.edit">
            Edit
        </button>
        <button v-on:click="$set(todo, 'edit', !todo.edit), $emit('edit-todo', todo)" v-if="todo.edit">
            Save
        </button>
        <button class="remove" v-on:click="$emit('remove-todo', todo.id)">X</button>
    </li>
</template>

<script>
export default({
    props: {
        todo: {
            type: Object,
            required: true,
            edit: false
        },
        index: Number
    }
})
</script>

<style scoped>
    li {
        border: 1px solid;
        display: flex;
        justify-content: space-between;
        padding: 2rem 2rem;
        margin: 1rem 15rem;
    }
    .remove {
        background: red;
        color: #fff;
        border-radius: 50%;
        font-weight: bold;
    }
    .done {
        text-decoration: line-through;
    }
    input {
        margin-right: 1rem;
    }
</style>