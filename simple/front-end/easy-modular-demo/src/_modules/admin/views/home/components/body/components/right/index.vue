<template>
  <div class="em-admin-home-body-right">
    <right-top></right-top>
    <right-bottom></right-bottom>
  </div>
</template>

<script>
import rightTop from './right-top/index'
import rightBottom from './right-bottom/index'
export default {
  components: { rightTop, rightBottom }
}
</script>

<style lang="scss" scoped>
.em-admin-home-body-right {
  width: 40%;
}
</style>
