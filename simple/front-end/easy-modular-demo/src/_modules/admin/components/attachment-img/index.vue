<template>
  <div :style="{ width, height }" class="em-attachment-img-preview">
    <img v-if="url" :src="url" />
    <em-icon class="no-picture" v-else name="photo" />
  </div>
</template>
<script>
// 接口
const api = $api.admin.attachment

export default {
  data() {
    return {
      url: ''
    }
  },
  props: {
    id: {
      type: String
    },
    width: {
      type: String,
      default: '100px'
    },
    height: {
      type: String,
      default: '100px'
    }
  },
  methods: {
    get() {
      if (this.id && this.id != this.$emptyGuid) {
        api.preview(this.id).then(url => {
          this.url = url
        })
      } else {
        this.url = ''
      }
    }
  },
  mounted() {
    this.get()
  },
  watch: {
    id() {
      this.get()
    }
  }
}
</script>
<style lang="scss">
.em-attachment-img-preview {
  position: relative;
  overflow: hidden;
  img {
    width: 100%;
    height: 100%;
    object-fit: cover;
  }
}
</style>
