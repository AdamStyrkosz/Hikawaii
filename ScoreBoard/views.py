import points as points
from django.shortcuts import render

# Create your views here.
from rest_framework.viewsets import ModelViewSet
from ScoreBoard.models import Score
from ScoreBoard.serializers import ScoreSerializer


class ScoreViewSet(ModelViewSet):
    queryset = Score.objects.order_by('-points')[:5]
    serializer_class = ScoreSerializer
