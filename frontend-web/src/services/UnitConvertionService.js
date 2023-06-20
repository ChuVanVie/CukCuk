import BaseService from "./base/BaseService";
import baseAPI from "@/resources/baseAPI";
import axios from "axios";

export default class UnitConvertionService extends BaseService {

    constructor(controller = "UnitConvertion") {
        super(controller);
    }

    getById(id) {
        try {
            return axios.get(baseAPI + this.controller + "/" + id);
        } catch (error) {
            console.log(error);
        }
    }
}