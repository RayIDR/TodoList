<template>
    <div>
        <TodoItem 
            v-for="(todo, i) of todos" :key="todo.title"
            v-bind:todo="todo"
            v-bind:index="i"
            v-on:remove-todo="removeTodo"
            v-on:edit-todo="editTodo"
        />
    </div>
</template>

<script>
import TodoItem from '@/components/TodoItem'
export default({
    props: ['todos'],
    components: {
        TodoItem
    }, 
    methods: {
        removeTodo(id) {
            this.$emit('remove-todo', id)
        },
        editTodo(todo) {
            this.$emit('edit-todo', todo)
        }
    }
})
</script>

<style scoped>
    ul {
        list-style: none;
        margin: 0;
        padding: 0;
    }
</style>