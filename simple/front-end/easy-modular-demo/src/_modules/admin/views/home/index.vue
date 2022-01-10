<template>
  <em-container class="home" scrollbar>
    <home-header></home-header>
    <home-body></home-body>
  </em-container>
</template>
<script>
import homeHeader from './components/header'
import homeBody from './components/body'

export default {
  components: { homeHeader, homeBody },
  data() {
    return {
      task: null,
      minute: 10
    }
  }
}
</script>
<style lang="scss" scoped>
.home {
  padding: 12px !important;
  box-sizing: border-box;
}
</style>
