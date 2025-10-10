namespace CSharp.Exercise;

public class TreeNode<T>
{
    public T Data { get; set; } // 해당 노드 데이터
    public List<TreeNode<T>> Children { get; set; } = new List<TreeNode<T>>(); // 자식 노드들
}

public class Tree
{
    
}