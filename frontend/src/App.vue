<template>
	<link rel="stylesheet" href="./src/assets/css/style.css">
	<Navbar />
	<div class="app">
		<!-- Sidebar -->
		<Sidebar />

		<!-- Content -->
		<router-view />
	</div>
</template>

<script setup>
import Navbar from './components/Navbar.vue';
import Sidebar from './components/Sidebar.vue';



</script>

<style lang="scss">

</style>