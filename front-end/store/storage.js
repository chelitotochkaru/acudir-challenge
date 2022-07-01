export const state = () => ({
  current: null,
  personas: []
})

export const mutations = {
  setCurrent(state, data) {
    state.current = data
  },
  add(state, item) {
    state.personas.push(item);
  },
  delete(state, index) {
    state.personas.splice(index, 1)
  }
}