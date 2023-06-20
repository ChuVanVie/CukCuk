<template>
    <div class="dialog-mask">
        <div class="dialog-container">
            <div class="dialog-header">
                <p>CUKCUK - Quản lý nhà hàng</p>
            </div>
            <div class="dialog-content items-center">
                <div style="margin: 0 12px;" v-if="this.type == 'delete' || this.type == 'update'">
                    <i class="fas fa-question-circle" style="font-size: 36px; color: #0973b9;"></i>
                </div>
                <div style="margin: 0 12px;"
                    v-if="this.type == 'warning' || this.type == 'warning2' || this.type == 'warning3'">
                    <i class="fas fa-exclamation-triangle" style="font-size: 36px; color: #ebef0b;"></i>
                </div>
                <div v-if="this.type == 'delete'">
                    <p>Bạn có chắc chắn muốn xóa Nguyên vật liệu {{ materialCode }} không?</p>

                </div>
                <div v-if="this.type == 'update'">
                    <p>Dữ liệu đã thay đổi, bạn có muốn cất không?</p>
                </div>
                <div v-if="this.type == 'warning'">
                    <p>Đơn vị tính chính không được trùng với đơn vị chuyển đổi.</p>
                </div>
                <div v-if="this.type == 'warning2'">
                    <p>Đơn vị chuyển đổi không được trùng với đơn vị tính chính.</p>
                </div>
                <div v-if="this.type == 'warning3'">
                    <p>Đơn vị chuyển đổi không được trùng nhau.</p>
                </div>
            </div>
            <div class="dialog-footer">
                <button v-if="this.type == 'warning' || this.type == 'warning2' || this.type == 'warning3'"
                    class="m-btn m-btn-footer align-center" style="min-width: 80px;" @click="btnOK">
                    <p>Đồng ý</p>
                </button>
                <button v-if="this.type == 'delete' || this.type == 'update'" class="m-btn m-btn-footer align-center"
                    style="min-width: 80px;" @click="btnAgree">
                    <p>Có</p>
                </button>
                <button v-if="this.type == 'delete' || this.type == 'update'" class="m-btn m-btn-footer align-center"
                    style="min-width: 80px;" @click="btnCancel">
                    <p>Không</p>
                </button>
                <button v-if="this.type == 'update'" class="m-btn m-btn-footer align-center" style="min-width: 80px;"
                    @click="btnDisable">
                    <p>Hủy bỏ</p>
                </button>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name: "GDialog",

    props: {
        materialCode: String,
        type: String
    },
    data() {
        return {
        }
    },
    methods: {
        btnOK() {
            this.$emit("clickOK");
        },
        btnAgree() {
            this.$emit("clickAgree");
        },
        btnCancel() {
            this.$emit("clickCancel");
        },
        btnDisable() {
            this.$emit("clickDisable");
        }
    }
}
</script>
<style scoped>
.dialog-mask {
    position: fixed;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    background-color: rgba(0, 0, 0, 0.542);
    z-index: 999;
}


.dialog-container {
    width: 400px;
    height: auto;
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    background-color: #0973b9;
    border-radius: 4px;
    padding: 6px;
}

.dialog-header {
    height: 30px;
    padding: 2px;
    border-color: #0973b9;
    color: #fff;
}

.dialog-content {
    height: 84px;
    background-color: #fff;
}

.dialog-footer {
    height: 42px;
    background-color: #fff;
    padding: 6px;
    display: flex;
    justify-content: end;
}
</style>