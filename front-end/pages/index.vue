<template>
  <b-container class="bv-example-row">
    <b-row align-h="center" align-v="center">
      <b-col>
        <img src="images/logo.png" class="logo" />
      </b-col>
    </b-row>

    <b-row>
      <b-col>
        <h4>Perfiles</h4>
      </b-col>
      <b-col>
        <h4>Perfiles vistos</h4>
      </b-col>
    </b-row>

    <b-row>
      <b-col>
        <Card></Card>
        <b-button variant="primary" @click="add">Siguiente</b-button>
      </b-col>

      <b-col>
        <b-row>
          <b-col v-for="persona in personas" :key="persona.id" cols="12">
            <Card editable="true"></Card>
          </b-col>
        </b-row>
      </b-col>
    </b-row>
  </b-container>
</template>

<style>
  .logo {
    height: 100px;
  }
</style>

<script>
import { mapMutations } from 'vuex'

export default {
  computed: {
    personas () {
      return this.$store.state.personas.list
    }
  },
  methods: {
    add(c) {
      this.$store.commit('personas/add', { id: this.$store.state.personas.list.length + 1, nombre: 'Marcelo' })
    }
  }
}
</script>