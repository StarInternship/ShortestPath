const defaultNodeColor = 'yellow';
const defaultEdgeColor = 'rgba(0, 0, 0, 0.1)';
const inPathNodeColor = 'red';
const inPathEdgeColor = 'green';
const sourceColor = 'blue';
const destinationColor = 'blue';
const defaultNodeRadius = 20;
const inPathNodeRadius = 70;
const defaultEdgeWidth = 5;
const InPathEdgeWidth = 30;
const shape = {
    'directed': {
        type: 'line',
        style: 'plain',
        head: 'arrow',
        tail: null
    },
    'undirected': {
        type: 'line',
        style: 'plain',
        head: null,
        tail: null
    }
};
var ogma;
var currentSubgraph;
var showingPath = false;
var graphSize;

$(document).ready(() => {
    getGraphList();
    $("input[name='graph_type']").change(() => redrawEdges());
});

function redrawEdges() {
    if (ogma == null) return;
    ogma.getEdges().forEach((edge, index) => edge.setAttributes({ shape: shape[$("input[name='graph_type']:checked").val()] }));
}

function getGraphList() {
    $.ajax({
        url: '/home/getGraphsList',
        method: 'post'
    }).done(result => {
        var list = result.List;
        $('#graphs_list').html('');
        $.each(list, (index, graphName) => $('#graphs_list').append('<option value="' + graphName + '">' + graphName + '</option>'));
        $('#import_button').html(
            '<button type="button" class="btn btn-outline-info" onclick="importGraph()">Import Graph</button>'
        );
        $('#main_graph_wrapper').html(
            '<button type="button" class="btn btn-outline-danger" onclick="showMainGraph()" disabled id="main_graph_button">Show Main Graph</button>'
        );
    });
}


function importGraph() {
    var graphName = $('#graph_name').val();

    $.ajax({
        url: '/home/importGraph',
        method: 'post',
        data: {
            graphName: graphName
        }
    }).done(graph => {
        showingPath = false;
        graphSize = 0;
        $('#main_graph_button').attr('disabled', false);
        $('#search_button').attr('disabled', false);

        var g = { nodes: [], edges: [] };
        $('#nodes_list').html('');
        $.each(graph.nodes, (index, node) => {
            graphSize++;
            g.nodes.push({
                id: node.id,
                attributes: {
                    x: node.attributes.x,
                    y: node.attributes.y,
                    text: node.attributes.text,
                    radius: defaultNodeRadius / 10,
                    color: defaultNodeColor
                }
            });
            $('#nodes_list').append('<option value="' + node.attributes.text + '">' + node.attributes.text + '</option>');
        });

        $.each(graph.edges, (index, edge) => {
            g.edges.push({
                id: edge.id,
                source: edge.source,
                target: edge.target,
                attributes: {
                    text: edge.attributes.text,
                    width: defaultEdgeWidth / 10,
                    color: defaultEdgeColor,
                    shape: shape[$("input[name='graph_type']:checked").val()]
                }
            });
        });

        ogma = new Ogma({
            graph: g,
            container: 'graph-container'
        });

        ogma.layouts.forceLink()
            .then(() => {
                ogma.view.locateGraph({
                    easing: 'linear',
                    duration: 1000
                });
            });

        currentSubgraph = { nodes: [], edges: [] };
    });
}

function showMainGraph() {
    if (showingPath) {
        showingPath = false;
        $.each(currentSubgraph.nodes, (index, node) => {
            ogma.getNode(node.id).setAttributes({ radius: defaultNodeRadius / 10, color: defaultNodeColor })
        });
        $.each(currentSubgraph.edges, (index, edge) => {
            ogma.getEdge(edge.id).setAttributes({ width: defaultEdgeWidth / 10, color: defaultEdgeColor })
        });
    }
}

function search() {
    var source = $('#source_node').val();
    var destination = $('#destination_node').val();
    var maxDistance = $('#max_distance').val();
    var findAllPaths = $("input[name='search_type']:checked").val() == 'all_paths';
    var directed = $("input[name='graph_type']:checked").val() == 'directed';

    if (showingPath) {
        showMainGraph();
    }

    $.ajax({
        url: '/home/search',
        method: 'post',
        data: {
            source: source,
            destination: destination,
            maxDistance: maxDistance,
            findAllPaths: findAllPaths,
            directed: directed
        }
    }).done(subgraph => {
        showingPath = true;
        currentSubgraph = subgraph;
        $.each(currentSubgraph.nodes, (index, node) => {
            ogma.getNode(node.id).setAttributes({ radius: inPathNodeRadius * Math.sqrt(graphSize * 10000) / 10000, color: node.id == source ? sourceColor : node.id == destination ? destinationColor : inPathNodeColor })
            console.log(node.id);
        });
        console.log('edges');
        $.each(currentSubgraph.edges, (index, edge) => {
            ogma.getEdge(edge.id).setAttributes({ width: InPathEdgeWidth * Math.sqrt(graphSize * 10000) / 10000, color: inPathEdgeColor })
            console.log(edge.id);
        });
    });
}