namespace ConsoleLeetCode;

public class TreeNode {
    public int val;
    public TreeNode? left;
    public TreeNode? right;

    public TreeNode(int value = 0, TreeNode? left = null, TreeNode? right = null)
    {
        val = value;
        this.left = left;
        this.right = right;
    }
}
