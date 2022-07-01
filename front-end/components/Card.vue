<template>
    <div class="pb-3">
        <b-card no-body class="overflow-hidden" style="max-width: 500px;" v-if=persona>
            <b-row no-gutters>
                <b-col md="4">
                    <b-card-img src="images/profile.jpg" alt="Image" class="rounded-0"></b-card-img>
                </b-col>
                <b-col md="8">
                    <b-card-body :title="persona.nombre">
                        <b-button variant="danger" v-if="editable" @click="remove">Eliminar</b-button>
                    </b-card-body>
                </b-col>
            </b-row>
        </b-card>
    </div>
</template>

<script>
export default {
    props: {
        index: Number,
        persona: Object,
        editable: Boolean,
    },
    methods: {
        async remove() {
            return fetch('http://localhost:8000/api/v1/personas/' + this.persona.id, { method: 'DELETE' })
                .then((res) => {
                    if (res.status == 200)
                        this.$store.commit('storage/delete', this.index)
                })
        }
    }
}
</script>