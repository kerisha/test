<template>
  <div>
    <h4>Updater User : {{ user.name }}</h4>
    <form @submit.prevent="updateUser">
      <label for="name">Name:</label>
      <input type="text" id="name" v-model="user.name" required />

      <label for="email">Email:</label>
      <input type="email" id="email" v-model="user.email" required />

      <button type="submit">Update</button>
    </form>
  </div>
</template>

<script>
import axios from "axios";
import { API_BASE_URL } from "../config";

export default {
  name: "UserDetails",
  data() {
    return {
      user: {
        id: null,
        name: "",
        email: "",
      },
    };
  },
  watch: {
    "$route.params.userId"(newVal, oldVal) {
      this.fetchUserDetails();
    },
  },
  methods: {
    fetchUserDetails() {
      const userId = this.$route.params.userId;

      axios
        .get(API_BASE_URL + `users/${userId}`)
        .then((response) => {
          this.user = response.data;
        })
        .catch((error) => {
          console.error("Error fetching user details: " + error);
        });
    },
    updateUser() {
      const userId = this.$route.params.userId;

      axios
        .put(API_BASE_URL + `users/${userId}`, this.user)
        .then((response) => {
          // Handle success, e.g., show a success message
          console.log("User details updated successfully");
          this.$router.push({ path: "/" });
        })
        .catch((error) => {
          // Handle error, e.g., show an error message
          console.error("Error updating user details: " + error);
        });
    },
  },
  created() {
    this.fetchUserDetails();
  },
};
</script>
