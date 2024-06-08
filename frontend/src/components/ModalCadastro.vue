<template>
  <main>
    <div class="container-fluid">
      <div class="row">
        <h2 class="my-4">Cadastro de Pedido de Venda</h2>
        <hr>
        <div class="progress mb-4">
          <div class="progress-bar" role="progressbar" :style="{ width: progress + '%' }" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100">{{ progress }}%</div>
        </div>

        <form v-if="step === 1" @submit.prevent="nextStep" class="mb-4">
          <!-- Formulário para cadastro do cliente -->
          <h4>Cadastro Cliente</h4>
          <div class="row g-3">
            <div class="col-md-6">
              <label for="cliente" class="form-label">Nome do Cliente:</label>
              <input type="text" class="form-control" id="cliente" v-model="cliente" required>
            </div>
            <div class="col-md-6">
              <label for="cpf" class="form-label">CPF:</label>
              <input type="text" class="form-control" id="cpf" v-model="cpf" required>
            </div>
            <div class="col-md-6">
              <label for="telefone" class="form-label">Telefone:</label>
              <input type="tel" class="form-control" id="telefone" v-model="telefone" required>
            </div>
            <div class="col-md-6">
              <label for="email" class="form-label">Email:</label>
              <input type="email" class="form-control" id="email" v-model="email" required>
            </div>
            <div class="col-md-6">
              <label for="cidade" class="form-label">Cidade:</label>
              <input type="text" class="form-control" id="cidade" v-model="cidade" required>
            </div>
            <div class="col-md-6">
              <label for="endereco" class="form-label">Endereço:</label>
              <input type="text" class="form-control" id="endereco" v-model="endereco" required>
            </div>
          </div>
          <button type="submit" class="btn btn-primary mt-3">Próxima Etapa</button>
        </form>

        <form v-else-if="step === 2" @submit.prevent="nextStep" class="mb-4">
          <!-- Formulário para cadastro do pedido -->
          <h4>Cadastro Pedido</h4>
          <div class="row g-3">
            <div class="col-md-6">
              <label for="nome_produto" class="form-label">Nome do Produto:</label>
              <select class="form-select" id="nome_produto" v-model="produtoSelecionadoId">
                <option value="">Selecione o Produto</option>
                <option v-for="produto in produtos" :key="produto.id" :value="produto.id">{{ produto.nome }}</option>
              </select>
            </div>
            <div class="col-md-6">
              <label for="quantidade" class="form-label">Quantidade:</label>
              <input type="number" class="form-control" id="quantidade" v-model.number="quantidade">
            </div>
          </div>
          <div class="row mt-3">
            <div class="col">
              <button v-if="produtosSelecionados.length > 0" type="button" class="btn btn-danger me-2" @click="removerProduto(index)">Remover Produtos</button>
              <button type="button" class="btn btn-primary" @click="adicionarProduto">Adicionar Produto</button>
            </div>
          </div>

          <!-- Tabela para exibir produtos selecionados -->
          <div class="row mt-4">
            <div class="col">
              <h5>Produtos Selecionados</h5>
              <table class="table">
                <thead>
                  <tr>
                    <th>Nome do Produto</th>
                    <th>Quantidade</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(produto, index) in produtosSelecionados" :key="index">
                    <td>{{ produto.nome }}</td>
                    <td>{{ produto.quantidade }}</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
          <div class="row mt-3">
            <div class="col">
              <button type="button" class="btn btn-secondary me-2" @click="prevStep">Voltar</button>
              <button type="submit" class="btn btn-primary">Próxima Etapa</button>
            </div>
          </div>
        </form>
        
        <form v-else-if="step === 3" @submit.prevent="nextStep" class="mb-4">
          <!-- Formulário para escolha do método de pagamento -->
          <h4>Método de Pagamento</h4>
          <div class="row g-3">
            <div class="col">
              <label class="form-check-label" for="metodo_pagamento">Selecione o Método de Pagamento:</label>
              <div class="form-check">
                <input class="form-check-input" type="radio" name="metodo_pagamento" id="metodo_pagamento_1" value="cartao" v-model="metodoPagamento">
                  <label class="form-check-label" for="metodo_pagamento_1">
                    Cartão de Crédito
                  </label>
              </div>
              <div class="form-check">
                  <input class="form-check-input" type="radio" name="metodo_pagamento" id="metodo_pagamento_2" value="boleto" v-model="metodoPagamento">
                  <label class="form-check-label" for="metodo_pagamento_2">
                    Boleto Bancário
                  </label>
              </div>
              <!-- Adicione outros métodos de pagamento conforme necessário -->
            </div>
          </div>
          <div class="row mt-3">
            <div class="col">
              <button type="button" class="btn btn-secondary me-2" @click="prevStep">Voltar</button>
              <button type="submit" class="btn btn-primary">Próxima Etapa</button>
            </div>
          </div>
        </form>
        
        <form v-else @submit.prevent="submitOrder" class="mb-4">
          <!-- Resumo do pedido e confirmação -->
          <h3>Resumo do Pedido:</h3>
          <div class="table-responsive">
            <table class="table table-striped">
              <tbody>
                <tr>
                  <th scope="row">Cliente:</th>
                  <td>{{ cliente }}</td>
                </tr>
                <tr>
                  <th scope="row">CPF:</th>
                  <td>{{ cpf }}</td>
                </tr>
                <tr>
                  <th scope="row">Telefone:</th>
                  <td>{{ telefone }}</td>
                </tr>
                <tr>
                  <th scope="row">Email:</th>
                  <td>{{ email }}</td>
                </tr>
                <tr>
                  <th scope="row">Cidade:</th>
                  <td>{{ cidade }}</td>
                </tr>
                <tr>
                  <th scope="row">Endereço:</th>
                  <td>{{ endereco }}</td>
                </tr>
                <tr>
                  <th scope="row">Forma de Pagamento:</th>
                  <td>{{ metodoPagamento }}</td>
                </tr>
              </tbody>
            </table>
          </div>
          <!-- Campos para exibir detalhes do pedido -->
          <div class="table-responsive mt-4">
            <table class="table table-striped">
              <thead>
                <tr>
                  <th scope="col">Nome do Produto</th>
                  <th scope="col">Quantidade</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(produto, index) in produtosSelecionados" :key="index">
                  <td>{{ produto.nome }}</td>
                  <td>{{ produto.quantidade }}</td>
                </tr>
              </tbody>
            </table>
          </div>
          <div class="row mt-3">
            <div class="col">
              <button type="button" class="btn btn-secondary me-2" @click="prevStep">Voltar</button>
              <button type="submit" class="btn btn-primary">Finalizar Cadastro</button>
            </div>
          </div>
        </form>
      </div>
    </div>
  </main>
</template>

<script>
import axios from 'axios'; 

export default {
  data() {
    return {
      cliente: '',
      cpf: '',
      telefone: '',
      email: '',
      cidade: '',
      endereco: '',
      metodoPagamento: '',
      produtoSelecionadoId: '',
      quantidade: 0,
      produtos: [
        { id: 1, nome: 'Produto 1' },
        { id: 2, nome: 'Produto 2' },
        { id: 3, nome: 'Produto 3' },
        // Adicione os produtos disponíveis aqui
      ],
      produtosSelecionados: [],
      step: 1,
      progress: 0
    };
  },
  methods: {
    nextStep() {
      if (this.step === 2 && this.produtosSelecionados.length === 0) {
        alert("Selecione pelo menos um produto antes de prosseguir.");
        return;
      }
      this.step++;
      this.progress = (this.step / 4) * 100; // 4 etapas
    },
    prevStep() {
      if (this.step > 1) {
        this.step--;
        this.progress = (this.step / 4) * 100; // 4 etapas
      }
    },
    adicionarProduto() {
      const produtoSelecionado = this.produtos.find(produto => produto.id === parseInt(this.produtoSelecionadoId));
      if (produtoSelecionado) {
        this.produtosSelecionados.push({
          id: produtoSelecionado.id,
          nome: produtoSelecionado.nome,
          quantidade: this.quantidade
        });
      }
      // Limpar campos após adicionar produto
      this.produtoSelecionadoId = '';
      this.quantidade = 0;
    },
    removerProduto(index) {
      this.produtosSelecionados.splice(index, 1);
    },
    submitOrder() {
      // Lógica para enviar pedido para o backend
      const produtosSelecionados = this.produtosSelecionados.map(produto => ({
        nome: produto.nome,
        quantidade: produto.quantidade
      }));

      // Exemplo de envio dos produtos selecionados para o backend usando axios
      axios.post('http://localhost:7103/api/pedidos', { produtos: produtosSelecionados })
        .then(response => {
          console.log(response.data);
          alert("Pedido enviado com sucesso!");
        })
        .catch(error => {
          console.error(error);
          alert("Erro ao enviar o pedido. Por favor, tente novamente.");
        });
    }
  },
  computed: {
    // Propriedades computadas
  }
};
</script>

<style scoped>
/* Estilos */
</style>
