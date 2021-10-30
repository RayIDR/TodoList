<template>
    <form @submit.prevent="onSubmit">
        <input type="text" v-model="title">
        <button type="submit">Add</button>
    </form>
</template>

<script>
export default {
    data() {
        return {
            title: ''
        }
    },
    methods: {
        onSubmit() {
            console.log('submit', this.title)
            if (this.title.trim()) {
                const newTodo = {
                    title: this.title,
                    completed: false
                }

                this.$emit('add-todo', newTodo)
                this.title = ''
            }
        }
    }
}
</script>


<style scoped>
    form {
        display: block;
        margin: auto;
        margin-bottom: 16px;
    }
    input {
        width: 400px;
    }
</style>