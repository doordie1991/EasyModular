<template>
  <div class="em-admin-header">
    <div class="em-admin-header-item"><em-panel-small label="我的待办" :value="model.enterpriseCount" icon="edit" more @more="$router.push({ name: 'powermonitor_enterprisemonitoring' })" /></div>
    <div class="em-admin-header-item"><em-panel-small label="车辆违规" :value="model.monitorDisposalCount" icon="car" more type="primary" /></div>
    <div class="em-admin-header-item"><em-panel-small label="消防演习" :value="model.dischargePortCount" icon="flag" more type="warning" /></div>
    <div class="em-admin-header-item"><em-panel-small label="危险作业" :value="model.monitorPolluteCount" icon="fire-fill" more type="danger" /></div>
    <div class="em-admin-header-item"><em-panel-small label="事件快报" :value="model.productLineCount" icon="alert-fill" more type="blue" /></div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      model: {
        enterpriseCount: 384,
        productLineCount: 4851,
        dischargePortCount: 995,
        monitorPolluteCount: 6447,
        monitorDisposalCount: 7112
      }
    }
  }
}
</script>
<style lang="scss">
.em-admin-header {
  display: flex;
  align-items: center;
  width: 100%;
  height: 120px;

  &-item {
    width: 20%;
    margin-right: 20px;
  }

  &-item:last-child {
    margin-right: 0;
  }
}
</style>
