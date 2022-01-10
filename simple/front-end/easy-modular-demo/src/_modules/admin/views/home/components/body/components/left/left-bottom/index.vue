<template>
  <em-panel class="em-home-body-left-bottom" v-bind="panel">
    <template v-slot:toolbar>
      <el-button-group>
        <el-button size="mini" plain @click="btnClick(1)">今日</el-button>
        <el-button size="mini" plain @click="btnClick(2)">本周</el-button>
        <el-button size="mini" plain @click="btnClick(3)">本月</el-button>
        <el-button size="mini" plain @click="btnClick(4)">全年</el-button> </el-button-group
      >&nbsp;&nbsp;
      <el-date-picker
        size="mini"
        v-model="date"
        valueFormat="yyyy-MM-dd"
        style="width: 221px !important"
        type="daterange"
        range-separator="-"
        start-placeholder="开始日期"
        end-placeholder="结束日期"
      ></el-date-picker>
    </template>
    <div class="em-home-body-left-bottom-chart">
      <div class="em-home-body-left-bottom-chart-l">
        <ve-pie :data="chart1.chartData" :settings="chart1.chartSettings" :extend="chart1.chartExtend" height="100%"></ve-pie>
      </div>
      <div class="em-home-body-left-bottom-chart-r">
        <ve-histogram :data="chart2.chartData" :settings="chart2.chartSettings" :extend="chart2.chartExtend" height="100%"></ve-histogram>
      </div>
    </div>
  </em-panel>
</template>

<script>
export default {
  data() {
    return {
      panel: {
        title: '隐患统计',
        icon: 'fire-fill',
        iconColor: '#f56c6c',
        height: '300px',
        header: true,
        noScrollbar: true
      },
      date: [],
      questionType: ['仓库安全', '公共区域管理', '化学品安全', '环境保护'],
      chart1: {
        chartSettings: {
          labelMap: {
            type: '问题类别',
            amount: '提报数量'
          }
        },
        chartExtend: {
          color: ['#3dd5e9', '#55a6f8', '#f8bd65', '#faa8a8'],
          legend: {
            show: false
          },
          series: {
            radius: '70%',
            center: ['50%', '50%'],

            labelLine: {
              show: true
            }
          }
        },
        chartData: {
          columns: ['type', 'amount'],
          rows: []
        }
      },
      chart2: {
        chartData: {
          columns: ['factory', 'amount'],
          rows: []
        },
        chartSettings: {
          labelMap: {
            factory: '厂区',
            amount: '问题点数'
          },
          itemStyle: {
            color: {
              type: 'linear',
              x: 0,
              y: 0,
              x2: 0,
              y2: 1,
              colorStops: [
                {
                  offset: 0,
                  color: '#7ee8f7'
                },
                {
                  offset: 1,
                  color: '#00acc1'
                }
              ]
            }
          }
        },
        chartExtend: {
          legend: {
            show: false
          },
          xAxis: {
            axisLabel: {
              textStyle: {
                color: '#909399'
              }
            }
          },
          yAxis: {
            axisLabel: {
              textStyle: {
                color: '#c0c4cc'
              }
            },
            splitLine: {
              show: true,
              lineStyle: {
                color: ['#e4e7ed'],
                width: 1
              }
            }
          },
          grid: {
            bottom: 5
          }
        }
      }
    }
  },
  methods: {
    getData() {
      this.chart1.chartExtend.legend.data = []
      this.chart1.chartData.rows = []
      for (let index = 0; index < 4; index++) {
        this.chart1.chartData.rows.push({ type: this.questionType[index], amount: Math.floor(Math.random() * (10 - 100)) + 100 })
      }

      this.chart2.chartData.rows = []
      this.chart2.chartData.rows.push({ factory: '一厂', amount: 100 })
      this.chart2.chartData.rows.push({ factory: '二厂', amount: 110 })
      this.chart2.chartData.rows.push({ factory: '三厂', amount: 200 })
      this.chart2.chartData.rows.push({ factory: '四厂', amount: 130 })
      this.chart2.chartData.rows.push({ factory: '五厂', amount: 300 })
      this.chart2.chartData.rows.push({ factory: '六厂', amount: 90 })
      this.chart2.chartData.rows.push({ factory: '七厂', amount: 220 })
      this.chart2.chartData.rows.push({ factory: '八厂', amount: 160 })
      this.chart2.chartData.rows.push({ factory: '九厂', amount: 70 })
      this.chart2.chartData.rows.push({ factory: '十厂', amount: 155 })
    },
    refresh() {
      this.getData()
    },
    btnClick(type) {
      this.date = []
      let day = this.$dayjs()
      let weekNumber = parseInt(this.$dayjs().format('d'))
      let firstDay = ''
      let lastDay = ''
      switch (type) {
        // 今日
        case 1:
          firstDay = day
          lastDay = day
          break
        // 本周
        case 2:
          if (weekNumber > 0) {
            firstDay = this.$dayjs().add(1 - weekNumber, 'day')
            lastDay = this.$dayjs().add(7 - weekNumber, 'day')
          } else {
            firstDay = this.$dayjs().add(-6, 'day')
            lastDay = this.$dayjs()
          }
          break
        // 本月
        case 3:
          firstDay = this.$dayjs().startOf('month')
          lastDay = this.$dayjs().endOf('month')
          break
        // 全年
        case 4:
          firstDay = this.$dayjs().startOf('year')
          lastDay = this.$dayjs().endOf('year')
          break
      }
      this.date.push(this.$dayjs(firstDay).format('YYYY-MM-DD'))
      this.date.push(this.$dayjs(lastDay).format('YYYY-MM-DD'))
    }
  },
  created() {
    this.getData()
  }
}
</script>

<style lang="scss">
.em-home-body-left-bottom {
  width: 100%;
  height: 100%;
  margin-top: 16px;
  &-chart {
    display: flex;
    width: 100%;
    height: 100%;
    &-l {
      width: 50%;
      height: 100%;
    }
    &-r {
      flex-grow: 1;
      height: 100%;
    }
  }
}
</style>
