
#Orthogonaltiy: Tool for decomposing objects into combinations of simpler objects in a structures way.
def is_orthogonal_to(self, v, tolerance=1e-10):
    return abs(self.dot(v)) < tolerance

def is_parallel_to(self, v):
    return (self.is_zero() or v.is_zero() or self.angle_with(v) == 0 or self.angle_with(v) == pi)

def is_zero(self, tolerance=1e-10):
    return self.magnitute() < tolerance

def dot(self, v):
    return sum([x * y for x,y in zip(self.coordinates, v.coordinates)])


def component_orthogonal_to(self, basis):
    try:
        projection = self.component_parallel_to(basis)
        return self.minus(projection)
    
    except Exception as e:
        if str(e) == self.NO_UNIQUE_PARALLEL_COMPONENT_MSG:
            raise Exception(self.NO_UNIUQ_ORTHOGONAL_COMPONENT_MSG)
        else:
            raise e 