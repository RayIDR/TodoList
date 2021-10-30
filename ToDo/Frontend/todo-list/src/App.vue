<template>
  <div id="app">
    <h1>ToDo List</h1>
    <AddTodo 
      @add-todo="addTodo"
    />
    <select v-model="filter">
      <option value="all">All</option>
      <option value="completed">Completed</option>
      <option value="in-progress">In Progress</option>
    </select>
    <hr>
    <TodoList 
      v-bind:todos="filteredTodos"
      @remove-todo="removeTodo"
      @edit-todo="editTodo"
    />
  </div>
</template>

<script>
import TodoList from '@/components/TodoList'
import AddTodo from '@/components/AddTodo'
export default {
  name: 'App',
  data() {
    return {
      todos: [],
      filter: 'all'
    }
  },
  components: {
    TodoList,
    AddTodo
  },
  created() {
      this.fetchData();
  },
  watch: {
      '$route': 'fetchData'
  },
  computed: {
    filteredTodos() {
      var filter;
      if (this.filter === 'all') {
        return this.todos;
      }
      if (this.filter === 'completed') {
        return this.todos.filter(t => t.completed);
      }
      if (this.filter === 'in-progress') {
        return this.todos.filter(t => !t.completed);
      }
      return filter;
    }
  },
  methods: {
      removeTodo(id) {
          this.todos = this.todos.filter(t => t.id !== id)
          fetch('task/' + id, {
            method: 'DELETE',
            body: JSON.stringify(id),
            headers: {
              'Content-Type': 'application/json'
            }
          });
      },
      addTodo(todo) {
          this.todos.push(todo);
          fetch('task', {
            method: 'POST',
            body: JSON.stringify(todo),
            headers: {
              'Content-Type': 'application/json'
            }
          });
          this.fetchData();
      },
      editTodo(todo) {
        fetch('task/' + todo.id, {
            method: 'PUT',
            body: (todo.id, JSON.stringify(todo)),
            headers: {
              'Content-Type': 'application/json'
            }
          });
      },
      fetchData() {

          fetch('task')
              .then(r => r.json())
              .then(json => {
                  this.todos = json;
                  return;
              });
      }
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
