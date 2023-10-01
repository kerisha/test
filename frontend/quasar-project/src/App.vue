<template>
  <div>
    <div>
      <h4>Users...</h4>
      <ul>
        <li v-for="user in users" :key="user.id" style="font-size: 1.5rem">
          <router-link
            :to="{ name: 'userDetails', params: { userId: user.id } }"
          >
            {{ user.name }}
          </router-link>
        </li>
      </ul>
      <h4>Add New User</h4>
      <form @submit.prevent="addUser">
        <label for="name">Name:</label>
        <input type="text" id="name" v-model="newUser.name" required />

        <label for="email">Email:</label>
        <input type="email" id="email" v-model="newUser.email" required />

        <button type="submit">Add User</button>
      </form>
    </div>
    <router-view />
  </div>
</template>

<script>
import axios from "axios"; // Import the Axios instance
import { API_BASE_URL } from "./config";

export default {
  data() {
    return {
      users: [],
      newUser: {
        name: "",
        email: "",
      },
      componentKey: 0,
    };
  },
  watch: {
    $route(to, from) {
      if (from.path !== "/" && to.path === "/") {
        console.log(from.path);
        // Trigger a refresh
        console.log("we here");
        this.$router.go();
      }
    },
  },
  methods: {
    addUser() {
      // Make an HTTP POST request to add a new user
      axios
        .post(API_BASE_URL + "users", this.newUser)
        .then((response) => {
          // Handle success, e.g., show a success message
          console.log("User added successfully");
          // Clear the form
          this.newUser.name = "";
          this.newUser.email = "";
          window.location.reload();
        })
        .catch((error) => {
          // Handle error, e.g., show an error message
          console.error("Error adding user: " + error);
        });
    },
  },
  mounted() {
    // Make an HTTP GET request to fetch users
    axios
      .get(API_BASE_URL + "users")
      .then((response) => {
        this.users = response.data;
      })
      .catch((error) => {
        console.error("Error fetching users: " + error);
      });
  },
};
</script>
