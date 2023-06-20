<template>
    <div class="dialog-mask">
        <div class="dialog-container">
            <div class="dialog-header">
                <p>CUKCUK - Quản lý nhà hàng</p>
            </div>
            <div class="dialog-content items-center">
                <div style="margin: 0 12px;">
                    <i class="fas fa-exclamation-circle" style="font-size: 36px; color: red;"></i>
                </div>
                <div>
                    <p>Có lỗi xảy ra! Vui lòng liên hệ Admin để được giải quyết.</p>
                </div>
            </div>
            <div class="dialog-footer">
                <button class="m-btn m-btn-footer align-center" style="min-width: 80px;" @click="btnAgree">
                    <p>Đồng ý</p>
                </button>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name: "GDialogErrorApi",

    props: {
        materialCode: String,
        type: String
    },
    data() {
        return {
        }
    },
    methods: {
        btnAgree() {
            this.$emit("clickAgree");
        },
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