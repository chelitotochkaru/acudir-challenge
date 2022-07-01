export const state = () => ({
  list: []
})

export const mutations = {
  search(state) {
    console.log('do search')
  },

  add(state, item) {
    console.log(item)
    state.list.push(item);
  }
}