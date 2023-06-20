<template>
    <div class="m-mask">
        <div id="form-container">
            <div id="form-header">
                <p>Nhập khẩu Nguyên vật liệu</p>
                <div class="align-center tooltip" @click="btnCloseForm"
                    style="background-color: #fff; width: 18px; height: 18px; border-radius: 8px; cursor: pointer;">
                    <i class="fas fa-times" style="color: #0973b9;"></i>
                    <span class="tooltiptext">Đóng</span>
                </div>
            </div>
            <div id="form-content">
                <p style="font-weight: bold; margin-bottom: 8px;">Bước 1: Chọn tệp nguồn</p>
                <div class="flex" style="height: calc(100% - 34px);">
                    <div class="flex-c" style="width: 170px; height: 100%; padding-right: 4px; background-color: #f5f5f5;">
                        <div class="import-step">
                            <p style="font-weight: bold;">1. Chọn tệp nguồn</p>
                        </div>
                        <div class="import-step">
                            <p style="font-weight: bold;">2. Kiểm tra dữ liệu</p>
                        </div>
                        <div class="import-step">
                            <p style="font-weight: bold;">3. Kết quả nhập khẩu</p>
                        </div>
                    </div>
                    <div style="width: calc(100% - 170px); height: 100%; padding: 8px; border: 1px solid #bbb;">
                        <p>Chọn dữ liệu Nguyên vật liệu đã chuẩn bị để nhập khẩu vào phần mềm</p>
                        <div class="flex" style="margin: 8px 0;">
                            <input type="text" disabled style="width: 500px; margin-right: 12px;">
                            <label for="fileSelect"
                                style="padding: 2px 24px; border: 1px solid #bbb; border-radius: 2px; cursor: pointer;">Chọn</label>
                            <input id="fileSelect" type="file"
                                accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"
                                style="display: none;" />
                        </div>
                        <p>Chưa có tệp mẫu để chuẩn bị dữ liệu? Tải tệp excel mẫu mà phần mềm cung cấp để chuẩn bị dữ liệu
                            nhập khẩu <a href="#">Tại đây</a></p>
                    </div>
                </div>
            </div>
            <div id="form-footer" class="space-between">
                <button class="m-btn m-btn-footer align-center" style="margin-left: 16px;">
                    <div class="icon-help"></div>
                    <p>Giúp</p>
                </button>
                <div class="flex">
                    <button class="m-btn m-btn-footer align-center">
                        <div class="icon-undo"></div>
                        <p>Quay lại</p>
                    </button>
                    <button class="m-btn m-btn-footer align-center">
                        <p>Tiếp tục</p>
                        <div class="icon-continue"></div>
                    </button>
                    <button class="m-btn m-btn-footer align-center" @click="btnCloseForm">
                        <div class="icon-cancel"></div>
                        <p>Hủy bỏ</p>
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name: "MaterialFormImport",
    components: {

    },
    props: {

    },
    data() {
        return {

        }
    },
    methods: {
        btnCloseForm() {
            this.$emit("closeForm");
        }
    },
}
</script>
<style scoped>
#form-container {
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    background-color: #0973b9;
    width: 962px;
    height: auto;
    border-radius: 6px;
    padding: 6px;
}

#form-header {
    height: 30px;
    padding: 0 4px;
    border-color: #0973b9;
    color: #fff;
    background-color: #0973b9;
    display: flex;
    align-items: center;
    justify-content: space-between;
}

#form-content {
    height: 520px;
    background-color: #fff;
    padding: 8px 8px 8px 16px;
}

#form-footer {
    height: 30px;
    background-color: #fff;
    padding: 8px 0;
}

.import-step {
    padding: 4px 12px;
    margin-bottom: 1px;
    border: 1px solid #bbb;
    border-radius: 2px;
    cursor: pointer;
}

.import-step:hover {
    border-color: #fbb13c;
    background-color: #ffe69f;
}

.tooltip {
    position: relative;
    display: inline-block;
}

.tooltip .tooltiptext {
    visibility: hidden;
    width: max-content;
    font-size: 12px;
    background-color: black;
    color: #fff;
    text-align: center;
    border-radius: 4px;
    padding: 5px;

    /* Position the tooltip */
    position: absolute;
    top: -50%;
    left: 100%;
}

.tooltip:hover .tooltiptext {
    visibility: visible;
}

.icon-help {
    background: url('../../assets/img/IconSprite.png') no-repeat 0 -1600px;
    width: 16px;
    height: 16px;
    margin-right: 4px;
}

.icon-undo {
    background: url('../../assets/img/IconSprite.png') no-repeat 0 -1024px;
    width: 15px;
    height: 15px;
    margin-right: 4px;
}

.icon-continue {
    background: url('../../assets/img/IconSprite.png') no-repeat 0 -832px;
    width: 15px;
    height: 15px;
    margin-left: 4px;
}

.icon-cancel {
    background: url('../../assets/img/IconSprite.png') no-repeat 0 -1968px;
    width: 15px;
    height: 15px;
    margin-right: 4px;
}
</style>