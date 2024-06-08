<template>
  <main>
    <div class="container-fluid">
      <h2>PESQUISA DE PEDIDO</h2>
      <div class="row">
        <h3 class="my-4">Filtros Rápidos</h3>
        <!-- Inputs de Data e Pesquisa -->
        <div class="mb-4 d-flex align-items-center">
          <div class="me-3 d-flex align-items-center">
            <label for="dataInicio" class="form-label me-2">Data Entrada:</label>
            <input type="date" id="dataInicio" v-model="dataInicio" class="form-control me-2" />
            <button @click="filtrarPorData('entrada')" class="btn btn-primary">Filtrar</button>
          </div>

          <div class="me-3 d-flex align-items-center">
            <label for="dataFim" class="form-label me-2">Data Saída:</label>
            <input type="date" id="dataFim" v-model="dataFim" class="form-control me-2" />
            <button @click="filtrarPorData('saida')" class="btn btn-primary">Filtrar</button>
          </div>

          <div class="me-2">
            <input type="text" v-model="consultaBusca" placeholder="Pesquisar por cliente" class="form-control" />
          </div>
          <div>
            <button @click="aplicarFiltros" class="btn btn-primary">Buscar</button>
          </div>
          
          <!-- Checkboxes para Status -->
          <div class="ms-auto">
            <label for="aprovado" class="form-check-label me-2">Aprovado</label>
            <input type="checkbox" id="aprovado" class="form-check-input custom-checkbox" @change="filtrarPorStatus('Aprovado')" />
            
            <label for="rejeitado" class="form-check-label me-2">Rejeitado</label>
            <input type="checkbox" id="rejeitado" class="form-check-input custom-checkbox" @change="filtrarPorStatus('Rejeitado')" />
            
            <label for="pendente" class="form-check-label me-2">Pendente</label>
            <input type="checkbox" id="pendente" class="form-check-input custom-checkbox" @change="filtrarPorStatus('Pendente')" />
            
            <label for="emAndamento" class="form-check-label me-2">Em Andamento</label>
            <input type="checkbox" id="emAndamento" class="form-check-input custom-checkbox" @change="filtrarPorStatus('Em andamento')" />
          </div>
        </div>
        <hr>    
        <div class="table-responsive">
          <table class="table table-striped table-bordered tabela-pedido">
            <thead class="cara">
              <tr class="teste">
                <th colspan="8" class="text-center">Lista de Pedidos de Venda</th>
              </tr>
              <tr>
                <th scope="col" class="py-3 px-2 text-lg">Código</th>
                <th scope="col" class="py-3 px-4 text-lg">Cliente</th>
                <th scope="col" class="py-3 px-4 text-lg">Qtd</th>
                <th scope="col" class="py-3 px-4 text-lg">Valor</th>
                <th scope="col" class="py-3 px-4 text-lg">Método</th>
                <th scope="col" class="py-3 px-4 text-lg">Status</th>
                <th scope="col" class="py-3 px-4 text-lg">Ações</th>
              </tr>
            </thead>
            <tbody>
              <!-- Loop apenas se houver pedidos -->
              <tr v-for="(pedido, index) in pedidosFiltrados" :key="pedido.id_pedido">
                <td>{{ pedido.id_pedido }}</td>
                <td>{{ pedido.cliente ? pedido.cliente.nome : '' }}</td>
                <td>{{ pedido.qtdeProduto }}</td>
                <td>{{ pedido.valorTotal }}</td>
                <td>{{ pedido.formaPagamento }}</td>
                <td>
                  <span
                    :class="{
                      'badge bg-success': pedido.statusPedido === 'Aprovado',
                      'badge bg-danger': pedido.statusPedido === 'Rejeitado',
                      'badge bg-warning': pedido.statusPedido === 'Pendente',
                      'badge bg-info': pedido.statusPedido === 'Em andamento',
                    }"
                    >{{ pedido.statusPedido }}</span
                  >
                </td>
                <td>
                  <button class="btn btn-warning btn-sm me-1" @click="editarPedido(pedido)">Editar</button>
                  <button class="btn btn-danger btn-sm me-1" @click="excluirPedido(pedido.id_pedido)">Excluir</button>
                  <button class="btn btn-info btn-sm" @click="verDetalhes(pedido)">Detalhes</button>
                </td>
              </tr>
              <!-- Mensagem de "Nenhum pedido encontrado" sempre visível -->
              <tr v-if="pedidosFiltrados.length === 0">
                <td colspan="7" class="text-center">Nenhum pedido encontrado.</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
      <nav aria-label="Navegação de página exemplo">
        <ul class="pagination justify-content-center">
          <li class="page-item disabled">
            <a class="page-link" href="#" tabindex="-1">Anterior</a>
          </li>
          <li class="page-item">
            <a class="page-link" href="#">1</a>
          </li>
          <li class="page-item">
            <a class="page-link" href="#">2</a>
          </li>
          <li class="page-item">
            <a class="page-link" href="#">3</a>
          </li>
          <li class="page-item">
            <a class="page-link" href="#">Próximo</a>
          </li>
        </ul>
      </nav>

      <!-- Modal de Detalhes -->
      <div class="modal fade" id="pedidoModal" tabindex="-1" aria-labelledby="pedidoModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-xl">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="pedidoModalLabel">Detalhes do Pedido</h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              <div v-if="Object.keys(pedidoDetalhes).length === 0">
                               <p>Nenhum pedido selecionado.</p>
              </div>
              <div v-else>
                <h5>Cliente: {{ pedidoDetalhes.cliente ? pedidoDetalhes.cliente.nome : '' }}</h5>
                <p>Código do Pedido: {{ pedidoDetalhes.id_pedido }}</p>
                <p>Data de Entrada: {{ formatarData(pedidoDetalhes.data_entrada_pedido) }}</p>
                <p>Data de Saída: {{ formatarData(pedidoDetalhes.data_saida_pedido) }}</p>
                <p>Quantidade: {{ pedidoDetalhes.qtdeProduto }}</p>
                <p>Valor Total: {{ pedidoDetalhes.valorTotal }}</p>
                <p>Forma de Pagamento: {{ pedidoDetalhes.formaPagamento }}</p>
                <p>Status: {{ pedidoDetalhes.statusPedido }}</p>
                <h5>Produtos:</h5>
                <ul>
                  <li v-for="produto in pedidoDetalhes.produtos" :key="produto.id">
                    {{ produto.nome }} - {{ produto.quantidade }} x {{ produto.preco }}
                  </li>
                </ul>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Fechar</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </main>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      pedidos: [],
      consultaBusca: '',
      linhasPorPagina: 35,
      pedidoDetalhes: {},
      dataInicio: '',
      dataFim: '',
      filtrosAplicados: false
    };
  },
  computed: {
    pedidosFiltrados() {
      if (!this.filtrosAplicados) {
        return this.pedidos.slice(0, this.linhasPorPagina);
      }

      return this.pedidos.filter((pedido) => {
        const nomeCliente = pedido.cliente ? pedido.cliente.nome.toLowerCase() : '';
        const dataPedido = new Date(pedido.data_entrada_pedido);
        const dataInicio = this.dataInicio ? new Date(this.dataInicio) : null;
        const dataFim = this.dataFim ? new Date(this.dataFim) : null;

        const nomeIncluiBusca = nomeCliente.includes(this.consultaBusca.toLowerCase());
        const dentroDoIntervalo = (!dataInicio || dataPedido >= dataInicio) && (!dataFim || dataPedido <= dataFim);

        return nomeIncluiBusca && dentroDoIntervalo;
      }).slice(0, this.linhasPorPagina);
    }
  },
  methods: {
    buscarPedidos() {
      axios
        .get('https://localhost:7103/api/pedidos/')
        .then((response) => {
          this.pedidos = response.data;
        })
        .catch((error) => {
          console.error('Erro ao buscar pedidos:', error);
        });
    },
    aplicarFiltros() {
      this.filtrosAplicados = true;
    },
    verDetalhes(pedido) {
      this.pedidoDetalhes = pedido;
      const modal = new bootstrap.Modal(document.getElementById('pedidoModal'));
      modal.show();
    },
    editarPedido(pedido) {
      console.log('Editar pedido:', pedido);
      // Ainda a fazer 
    },
    excluirPedido(idPedido) {
      console.log('Excluir pedido com ID:', idPedido);
      //  Ainda a fazer
    },
    formatarData(data) {
      return new Date(data).toLocaleDateString('pt-BR');
    },
    filtrarPorData(tipo) {
      if (tipo === 'entrada') {
        this.pedidos = this.pedidos.filter(pedido => pedido.data_entrada_pedido === this.dataInicio);
      } else if (tipo === 'saida') {
        this.pedidos = this.pedidos.filter(pedido => pedido.data_saida_pedido === this.dataFim);
      }
      this.filtrosAplicados = true;
    },
    filtrarPorStatus(status) {
      if (status === 'Aprovado') {
        this.pedidosFiltrados = this.pedidos.filter(pedido => pedido.statusPedido === 'Aprovado');
      } else if (status === 'Rejeitado') {
        this.pedidosFiltrados = this.pedidos.filter(pedido => pedido.statusPedido === 'Rejeitado');
      } else if (status === 'Pendente') {
        this.pedidosFiltrados = this.pedidos.filter(pedido => pedido.statusPedido === 'Pendente');
      } else if (status === 'Em andamento') {
        this.pedidosFiltrados = this.pedidos.filter(pedido => pedido.statusPedido === 'Em andamento');
      }
    }
  },
  mounted() {
    this.buscarPedidos();
  },
};
</script>

<style scoped>
th {
  color: #fff;
  background-color: #1e293b;
}

  /* Estilo personalizado para os checkboxes */
  .custom-checkbox input[type="checkbox"] {
    opacity: 0;
    position: absolute;
  }

  .custom-checkbox label {
    position: relative;
    cursor: pointer;
    padding-left: 25px; /* Espaçamento à esquerda da label */
  }

  /* Estilizando o ícone do checkbox */
  .custom-checkbox label::before {
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    width: 20px; /* Tamanho do ícone */
    height: 20px; /* Tamanho do ícone */
    border: 1px solid #aaa; /* Cor da borda */
    background-color: #fff; /* Cor de fundo */
  }

  /* Estilo do ícone quando o checkbox está marcado */
  .custom-checkbox input[type="checkbox"]:checked + label::before {
    background-color: #007bff; /* Cor de fundo quando marcado */
  }

  /* Estilo do ícone quando o checkbox está marcado e passa o mouse */
  .custom-checkbox input[type="checkbox"]:checked + label:hover::before {
    background-color: #0056b3; /* Cor de fundo quando marcado e passa o mouse */
  }

  /* Estilo do ícone quando o checkbox está marcado e focado */
  .custom-checkbox input[type="checkbox"]:checked + label:focus::before {
    box-shadow: 0 0 0 3px rgba(0, 123, 255, 0.25); /* Sombra quando focado */
  }

  /* Estilo do ícone quando a label está desabilitada */
  .custom-checkbox input[type="checkbox"]:disabled + label::before {
    background-color: #e9ecef; /* Cor de fundo quando desabilitado */
    border-color: #adb5bd; /* Cor da borda quando desabilitado */
  }

  /* Estilo do ícone quando a label está desabilitada e passa o mouse */
  .custom-checkbox input[type="checkbox"]:disabled + label:hover::before {
    background-color: #e9ecef; /* Cor de fundo quando desabilitado e passa o mouse */
    border-color: #adb5bd; /* Cor da borda quando desabilitado e passa o mouse */
  }
</style>

