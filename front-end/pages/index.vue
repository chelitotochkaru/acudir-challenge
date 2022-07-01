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
        <Card :persona=persona></Card>
        <b-button variant="primary" @click="next">Siguiente</b-button>
      </b-col>

      <b-col>
        <b-row>
          <b-col v-for="(p, index) in personas" :key="p.id" cols="12">
            <Card :persona=p :index=index :editable=true></Card>
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

export default {
  computed: {
    persona() {
      return this.$store.state.storage.current
    },
    personas() {
      return this.$store.state.storage.personas
    },
  },
  async mounted() {
    await this.search()
  },
  methods: {
    async search() {
      return fetch('http://localhost:8000/api/v1/personas/random')
        .then(res => res.json())
        .then((data) => {
          this.$store.commit('storage/setCurrent', data)
        })
    },
    add(c) {
      this.$store.commit('storage/add', this.persona)
    },
    next() {
      this.search()
      this.add()
    }
  }
}
</script>