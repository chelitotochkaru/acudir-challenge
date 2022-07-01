<template>
    <div class="pb-3">
        <b-card no-body class="overflow-hidden" style="max-width: 500px;">
            <b-row no-gutters>
                <b-col md="4">
                    <b-card-img src="images/profile.jpg" alt="Image" class="rounded-0"></b-card-img>
                </b-col>
                <b-col md="8">
                    <b-card-body title="Nombre">
                        <b-button variant="danger" v-if="editable">Eliminar</b-button>
                    </b-card-body>
                </b-col>
            </b-row>
        </b-card>
    </div>
</template>

<script>
export default {
    props: {
        editable: Boolean
    }
}
</script>