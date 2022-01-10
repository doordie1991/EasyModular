<template>
  <div class="em-admin-home-body">
    <body-left></body-left>
    <body-right></body-right>
  </div>
</template>

<script>
import bodyLeft from './components/left'
import bodyRight from './components/right'
export default {
  components: { bodyLeft, bodyRight }
}
</script>
<style lang="scss" scoped>
.em-admin-home-body {
  display: flex;
  width: 100%;
  margin-top: 16px;
}
</style>
