<template>
	<div class="container-fluid">
	  <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
		<h1 class="h2">Dashboard</h1>
	  </div>
	  <div class="row mb-3">
		<div class="col-md-4">
		  <div class="card text-white bg-primary mb-3">
			<div class="card-body">
			  <h5 class="card-title">Pedidos Pendentes</h5>
			  <p class="card-text">{{ pedidosPendentes }}</p>
			</div>
		  </div>
		</div>
		<div class="col-md-4">
		  <div class="card text-white bg-warning mb-3">
			<div class="card-body">
			  <h5 class="card-title">Pedidos Abertos</h5>
			  <p class="card-text">{{ pedidosAbertos }}</p>
			</div>
		  </div>
		</div>
		<div class="col-md-4">
		  <div class="card text-white bg-success mb-3">
			<div class="card-body">
			  <h5 class="card-title">Pedidos Concluídos</h5>
			  <p class="card-text">{{ pedidosConcluidos }}</p>
			</div>
		  </div>
		</div>
	  </div>
	  <div class="row">
		<div class="col-md-6">
		  <div class="card mb-4">
			<div class="card-body">
			  <h5 class="card-title">Pedidos na Última Semana</h5>
			  <canvas id="chartPedidosSemanal"></canvas>
			</div>
		  </div>
		</div>
		<div class="col-md-6">
		  <div class="card mb-4">
			<div class="card-body">
			  <h5 class="card-title">Pedidos no Último Mês</h5>
			  <canvas id="chartPedidosMensal"></canvas>
			</div>
		  </div>
		</div>
	  </div>
	</div>
  </template>
  