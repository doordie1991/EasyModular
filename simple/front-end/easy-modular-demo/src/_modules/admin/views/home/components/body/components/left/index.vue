<template>
  <div class="em-admin-home-body-left">
    <left-top></left-top>
    <left-bottom></left-bottom>
  </div>
</template>

<script>
import leftTop from './left-top/index'
import leftBottom from './left-bottom/index'
export default {
  components: { leftTop, leftBottom }
}
</script>

<style lang="scss">
.em-admin-home-body-left {
  flex-grow: 1;
  margin-right: 20px;
}
</style>
