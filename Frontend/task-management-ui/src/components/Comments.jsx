import { useEffect, useState } from "react";

import {
    getComments,
    createComment,
    deleteComment
} from "../services/commentService";

function Comments() {

    const [comments, setComments] =
        useState([]);

    const [comment, setComment] =
        useState({
            commentText: "",
            workItemId: "",
            userId: ""
        });


    useEffect(() => {

        loadComments();

    }, []);


    const loadComments = async () => {

        try {

            const data =
                await getComments();

            setComments(data);

        } catch (error) {

            console.error(error);
        }
    };


    const handleChange = (e) => {

        setComment({
            ...comment,
            [e.target.name]:
                e.target.value
        });
    };


    const handleCreate = async (e) => {

        e.preventDefault();

        try {

            await createComment({

                commentText:
                    comment.commentText,

                workItemId:
                    Number(
                        comment.workItemId
                    ),

                userId:
                    Number(comment.userId)

            });

            alert("Comment added");

            setComment({
                commentText: "",
                workItemId: "",
                userId: ""
            });

            loadComments();

        } catch (error) {

            console.error(error);

            alert(
                "Unable to add comment"
            );
        }
    };


    const handleDelete = async (id) => {

        try {

            await deleteComment(id);

            loadComments();

        } catch (error) {

            console.error(error);
        }
    };


    return (

        <div className="page">

            <h2>Comments</h2>


            <form
                className="form-card"
                onSubmit={handleCreate}
            >

                <textarea
                    name="commentText"
                    placeholder="Enter comment"
                    value={
                        comment.commentText
                    }
                    onChange={handleChange}
                    required
                />


                <input
                    type="number"
                    name="workItemId"
                    placeholder="Task ID"
                    value={
                        comment.workItemId
                    }
                    onChange={handleChange}
                    required
                />


                <input
                    type="number"
                    name="userId"
                    placeholder="User ID"
                    value={comment.userId}
                    onChange={handleChange}
                    required
                />


                <button type="submit">
                    Add Comment
                </button>

            </form>


            <h3>Comment List</h3>


            <table>

                <thead>

                    <tr>

                        <th>ID</th>
                        <th>Comment</th>
                        <th>Task ID</th>
                        <th>User ID</th>
                        <th>Created At</th>
                        <th>Action</th>

                    </tr>

                </thead>


                <tbody>

                    {comments.map((item) => (

                        <tr
                            key={
                                item.commentId
                            }
                        >

                            <td>
                                {item.commentId}
                            </td>

                            <td>
                                {
                                    item.commentText
                                }
                            </td>

                            <td>
                                {
                                    item.workItemId
                                }
                            </td>

                            <td>
                                {item.userId}
                            </td>

                            <td>
                                {
                                    item.createdAt
                                }
                            </td>

                            <td>

                                <button
                                    className="delete-btn"
                                    onClick={() =>
                                        handleDelete(
                                            item.commentId
                                        )
                                    }
                                >
                                    Delete
                                </button>

                            </td>

                        </tr>

                    ))}

                </tbody>

            </table>

        </div>
    );
}

export default Comments;