import { useEffect, useState } from "react";

import {
    getTeams,
    createTeam,
    deleteTeam
} from "../services/teamService";

function Teams() {

    const [teams, setTeams] =
        useState([]);

    const [team, setTeam] =
        useState({
            teamName: "",
            description: ""
        });


    useEffect(() => {

        loadTeams();

    }, []);


    const loadTeams = async () => {

        try {

            const data =
                await getTeams();

            setTeams(data);

        } catch (error) {

            console.error(error);

        }
    };


    const handleChange = (e) => {

        setTeam({
            ...team,
            [e.target.name]:
                e.target.value
        });
    };


    const handleCreate = async (e) => {

        e.preventDefault();

        try {

            await createTeam(team);

            alert("Team created successfully");

            setTeam({
                teamName: "",
                description: ""
            });

            loadTeams();

        } catch (error) {

            console.error(error);

            alert("Unable to create team");
        }
    };


    const handleDelete = async (id) => {

        if (!window.confirm(
            "Delete this team?"
        )) {
            return;
        }

        try {

            await deleteTeam(id);

            loadTeams();

        } catch (error) {

            console.error(error);
        }
    };


    return (

        <div className="page">

            <h2>Team Management</h2>


            <form
                className="form-card"
                onSubmit={handleCreate}
            >

                <h3>Create Team</h3>

                <input
                    name="teamName"
                    placeholder="Team Name"
                    value={team.teamName}
                    onChange={handleChange}
                    required
                />

                <textarea
                    name="description"
                    placeholder="Description"
                    value={team.description}
                    onChange={handleChange}
                />

                <button type="submit">
                    Create Team
                </button>

            </form>


            <h3>Teams</h3>


            <table>

                <thead>

                    <tr>

                        <th>ID</th>
                        <th>Team Name</th>
                        <th>Description</th>
                        <th>Action</th>

                    </tr>

                </thead>

                <tbody>

                    {teams.map((team) => (

                        <tr key={team.teamId}>

                            <td>
                                {team.teamId}
                            </td>

                            <td>
                                {team.teamName}
                            </td>

                            <td>
                                {team.description}
                            </td>

                            <td>

                                <button
                                    className="delete-btn"
                                    onClick={() =>
                                        handleDelete(
                                            team.teamId
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

export default Teams;