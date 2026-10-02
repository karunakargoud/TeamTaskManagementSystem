import api from "./api";

export const getComments = async () => {

    const response =
        await api.get("/Comments");

    return response.data;
};


export const createComment = async (comment) => {

    const response =
        await api.post(
            "/Comments",
            comment
        );

    return response.data;
};


export const deleteComment = async (id) => {

    const response =
        await api.delete(
            `/Comments/${id}`
        );

    return response.data;
};